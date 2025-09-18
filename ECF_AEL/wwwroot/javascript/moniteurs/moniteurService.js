import { 
  ajouterMoniteur as apiAjouter, 
  listerMoniteurs as apiLister,
  supprimerMoniteur as apiSupprimer,
  updateActivite as apiUpdateActivite
} from "./moniteurApi.js";

export async function ajouterMoniteur(formValues) {
  if (!formValues.nom || !formValues.prenom) {
    throw new Error("Nom et prénom sont obligatoires.");
  }
  if (!formValues.dateNaissance) {
    throw new Error("Date de naissance est obligatoire.");
  }

  const moniteur = {
    nomMoniteur: formValues.nom,
    prenomMoniteur: formValues.prenom,
    dateNaissance: formValues.dateNaissance,
    dateEmbauche: formValues.dateEmbauche,
    activite: formValues.activite
  };

  return await apiAjouter(moniteur);
}

export async function listerMoniteurs() {
  return await apiLister();
}

export async function supprimerMoniteur(id) {
  return await apiSupprimer(id);
}

export async function modifierActivite(id, nouvelleActivite) {
  return await apiUpdateActivite(id, nouvelleActivite);
}
