import { ajouterModele as apiAjouter, listerModeles as apiLister } from "./modeleApi.js";

export async function ajouterModele(formValues) {
  if (!formValues.nomModeleVehicule || formValues.nomModeleVehicule.length > 50) {
    throw new Error("Nom du modèle obligatoire et max 50 caractères.");
  }

  if (!formValues.marque || formValues.marque.length > 50) {
    throw new Error("Marque obligatoire et max 50 caractères.");
  }

  if (!formValues.annee || formValues.annee.length > 4 || isNaN(formValues.annee)) {
    throw new Error("Année obligatoire, numérique et max 4 caractères.");
  }

  if (!formValues.dateAchat) {
    throw new Error("Date d'achat obligatoire.");
  }

  return await apiAjouter(formValues);
}

export async function listerModeles() {
  return await apiLister();
}
