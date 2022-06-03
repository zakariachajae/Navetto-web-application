using Navette.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Navette.Controllers
{
    public class HomeController : Controller
    {
        public static List<Offres> offres = new List<Offres>();
        public static List<CarteNavetto> cartes = new List<CarteNavetto>();


        public ActionResult Index()


        {
            if (offres.Count == 0)
            {
                ViewBag.msg = "y a aucun trajet";
                Offres o = new Offres()
                {
                    PeriodeAbonnement = 30,
                    HDepart = DateTime.Now,
                    HArrive = DateTime.Now,
                    VilleDepart = "kenitra",
                    VilleArrive = "rabat",
                    Desc = "dfdfdfsd",
                    NbrVoulu = 50,
                    Id = offres.Count + 1,
                    Prix = 30,
                    valable = true
                };
                offres.Add(o);
            }

            ViewBag.Offres = offres;
            return View();
        }





        public ActionResult ajout()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ajout(int i = 0)
        {

            string periode = Request.Form["periode"];
            string HDepart = Request.Form["heure depart"];
            string HArrivee = Request.Form["heure arrive"];
            string VDepart = Request.Form["ville depart"];
            string VArrive = Request.Form["ville arrive"];
            string desc = Request.Form["description"];
            string nbrVoulu = Request.Form["nbr voulu"];
            string prix = Request.Form["prix"];


            if (!String.IsNullOrEmpty(VArrive) && !String.IsNullOrEmpty(VDepart)
                && !String.IsNullOrEmpty(periode) && !String.IsNullOrEmpty(HDepart)
                && !String.IsNullOrEmpty(HArrivee) && !String.IsNullOrEmpty(desc) &&
                !String.IsNullOrEmpty(nbrVoulu) && !String.IsNullOrEmpty(prix))
            {


                Offres nve = new Offres()
                {

                    PeriodeAbonnement = int.Parse(periode),
                    HDepart = Convert.ToDateTime(HDepart),
                    HArrive = Convert.ToDateTime(HArrivee),
                    VilleDepart = VDepart,
                    VilleArrive = VArrive,
                    Desc = desc,
                    NbrVoulu = int.Parse(nbrVoulu),
                    Id = offres.Count + 1,
                    Prix = int.Parse(prix),
                    valable = true
                };
                offres.Add(nve);

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.msg = "Veuillez verifier les champs saisis !!!!!!";
                return View();
            }

        }

        [HttpGet]
        public ActionResult modifer(int id)
        {
            // recuperer les infos de l'article en question
            Offres o = offres.Find(s => s.Id == id);
            ViewBag.o = o;
            return View(o);
        }

        [HttpPost]
        public ActionResult modifier(Offres o)
        {

            // recuperer les infos de l'article en question
            Offres o1 = offres.Find(s => s.Id == o.Id);

            // recuperer les donnees

            offres.Remove(o1);
            offres.Add(o);

            return RedirectToAction("Index", "Home");



        }
        public ActionResult supprimer(int id)
        {
            Offres o = offres.Find(s => s.Id == id);
            if (o != null)
            {

                ViewBag.o = o;
                return View();
            }


            TempData["notFound"] = "offre n'est pas trouvé !!!!!";
            return RedirectToAction("Index", "Home");

        }
        public ActionResult ConfSupprimer(int id)
        {
            Offres o = offres.Find(s => s.Id == id);
            if (o != null)
            {
                // transfert des donnees
                offres.Remove(o);
                return RedirectToAction("Index");
            }

            // transfert d'un msg entre actions
            TempData["notFound"] = "offre n'est pas trouvé !!!!!";
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Paiement(int id)
        {

            Offres o = offres.Find(s => s.Id == id);
            ViewBag.o = o;
            return View(o);
        }
        [HttpPost]
        public ActionResult Paiement(Offres o)
        {
            Offres o1 = offres.Find(s => s.Id == o.Id);

            if (o1 != null)
            {
                string TypeCarte = Request.Form["nom de carte"];
                string NumeroCarte = Request.Form["numero carte"];
                string DateExpiration = Request.Form["date expiration"];
                string Cvv = Request.Form["cvv"];
                if (!String.IsNullOrEmpty(TypeCarte) && !String.IsNullOrEmpty(NumeroCarte) &&
                    !String.IsNullOrEmpty(DateExpiration) && !String.IsNullOrEmpty(Cvv))
                {
                    CarteNavetto carte1 = new CarteNavetto()
                    {
                        Id = cartes.Count + 1,
                        ProprietaireCarte = "hamza",
                        Type = "premium",
                        NumeroCarte = 123456789,
                        DateExpiration = "12/2023",
                        CVV = 330
                    };

                    if (TypeCarte == carte1.Type && int.Parse(NumeroCarte) == carte1.NumeroCarte &&
                        DateExpiration == carte1.DateExpiration && int.Parse(Cvv) == carte1.CVV)
                    {
                        o1.NbrVoulu--;
                        Random rnd = new Random();
                        ViewBag.codeAchat = rnd.Next(1000, 10000);
                        ViewBag.o = o1;
                        return View();
                    }
                    else
                    {
                        ViewBag.o = o1;
                        ViewBag.error1 = "vos informations sont incorrectes, veuillez réessayer!";

                        return View();
                    }

                }
                else
                {
                    ViewBag.o = o1;
                    return View();
                }

            }
            return RedirectToAction("Index", "Home");


        }


    }



}

