using Navette.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Navette.Controllers
{
    public class DemandeController : Controller
    {
        public static List<Demande> demandes = new List<Demande>();

        public ActionResult MesDemandes()
        {
            if (demandes.Count == 0)
            {
                ViewBag.msg1 = "vous n'avez fais aucune demande";
                ViewBag.msg2 = "aucune demande n'etais faite";

            }
            if (demandes.Count != 0)
            {
                for (int i = 0; i < demandes.Count; i++)
                {
                    var d = demandes.ElementAt(i);
                    for (int j = i + 1; j < demandes.Count; j++)
                    {
                        if (d.VilleDepart.SequenceEqual(demandes.ElementAt(j).VilleDepart) && d.VilleArrive.SequenceEqual(demandes.ElementAt(j).VilleArrive) && !d.UserId.Equals(demandes.ElementAt(j).UserId))
                        {
                            demandes.RemoveAt(j);
                            d.nbrPersonneInteresse++;
                            ViewBag.message = "cette demande est deja faite, mais votre souhait sera bien enregisté ";
                            break;

                        }


                        else if (d.VilleDepart.SequenceEqual(demandes.ElementAt(j).VilleDepart) && d.VilleArrive.SequenceEqual(demandes.ElementAt(j).VilleArrive)
                             && d.UserId.Equals(demandes.ElementAt(j).UserId))
                        {
                            demandes.RemoveAt(j);

                            ViewBag.message1 = "Vous avez deja fait cette demande, essayer une autre demande ";
                            break;
                        }
                    }
                }
            }

            ViewBag.demandes = demandes;
            return View();
        }
        public ActionResult FaireDemande()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FaireDemande(int i = 0)
        {
            string periode = Request.Form["periode"];
            string HDepart = Request.Form["heure depart"];
            string HArrivee = Request.Form["heure arrive"];
            string VDepart = Request.Form["ville depart"];
            string VArrive = Request.Form["ville arrive"];



            if (!String.IsNullOrEmpty(VArrive) && !String.IsNullOrEmpty(VDepart)
                && !String.IsNullOrEmpty(periode) && !String.IsNullOrEmpty(HDepart)
                && !String.IsNullOrEmpty(HArrivee))
            {
                Int32.TryParse(periode, out int p);
                Demande nv = new Demande();

                nv.Id = demandes.Count + 1;
                nv.UserId = (int)Session["user_id"];
                nv.PeriodeAbonnement = p;
                nv.HeureDepart = Convert.ToDateTime(HDepart);
                nv.HeureArrive = Convert.ToDateTime(HArrivee);
                nv.VilleDepart = VDepart;
                nv.VilleArrive = VArrive;
                nv.NomDemandeur = (string)Session["username"];


                demandes.Add(nv);

                return RedirectToAction("MesDemandes", "Demande");
            }
            else
            {
                ViewBag.msg = "Veuillez verifier les champs saisis !!!!!!";
                return View();
            }

        }
        public ActionResult SupprimerDemande(int id)
        {
            Demande d = demandes.Find(s => s.Id == id);
            if (d != null)
            {
                ViewBag.d = d;
                return View();
            }
            return RedirectToAction("MesDemandes", "Demande");
        }
        public ActionResult ConfSupprimer(int id)
        {
            Demande d = demandes.Find(s => s.Id == id);
            if (d != null)
            {
                demandes.Remove(d);
                return RedirectToAction("MesDemandes", "Demande");
            }
            return RedirectToAction("MesDemandes", "Demande");
        }
        public ActionResult TrierDemandes()
        {
            return View();
        }




    }
}