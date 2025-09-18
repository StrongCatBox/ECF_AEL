import { 
  ajouterVehicule as apiAjouter, 
  listerVehicules as apiLister,
  supprimerVehicule as apiSupprimer,
  updateEtatVehicule as apiUpdateEtat
} from "./vehiculeApi.js";

// Ajouter un véhicule
export async function ajouterVehicule(formValues) {
  if (!formValues.numeroImmatriculation) {
    throw new Error("L'immatriculation est obligatoire.");
  }

  if (!formValues.modeleVehicule) {
    throw new Error("Le modèle est obligatoire.");
  }

  if (formValues.etat === undefined) {
    throw new Error("L'état est obligatoire.");
  }

  const vehicule = {
    numeroImmatriculation: formValues.numeroImmatriculation,
    modeleVehicule: formValues.modeleVehicule,
    etat: formValues.etat
  };

  return await apiAjouter(vehicule);
}

// Lister tous les véhicules
export async function listerVehicules() {
  return await apiLister();
}

// Supprimer un véhicule 
export async function supprimerVehicule(immatric) {
  return await apiSupprimer(immatric);
}

// Modifier l'état d'un véhicule 
export async function modifierEtatVehicule(immatric, nouvelEtat) {
  return await apiUpdateEtat(immatric, nouvelEtat);
}
