import { 
  ajouterLecon as apiAjouter, 
  listerEleves as apiListerEleves, 
  listerMoniteurs as apiListerMoniteurs, 
  listerModelesVehicules as apiListerModelesVehicules,
  listerCreneaux as apiListerCreneaux 
} from "./leconApi.js";

// Ajouter une leçon avec validation
export async function ajouterLecon(formValues) {
  if (!formValues.idEleve) {
    throw new Error("Un élève doit être sélectionné.");
  }

  if (!formValues.idMoniteur) {
    throw new Error("Un moniteur doit être sélectionné.");
  }

  if (!formValues.ModeleVehicule) {   
    throw new Error("Un modèle doit être sélectionné.");
  }

  if (!formValues.dateHeure) {
    throw new Error("La date et l'heure de la leçon sont obligatoires.");
  }

  if (!formValues.duree || isNaN(formValues.duree) || formValues.duree <= 0) {
    throw new Error("La durée doit être un nombre positif.");
  }

  return await apiAjouter(formValues);
}

// Récupérer élèves
export async function listerEleves() {
  return await apiListerEleves();
}

// Récupérer moniteurs
export async function listerMoniteurs() {
  return await apiListerMoniteurs();
}

// Récupérer modèles de véhicules
export async function listerModelesVehicules() {
  return await apiListerModelesVehicules();  
}
 // Récupérer le Calendrier
export async function listerCreneaux() {
  return await apiListerCreneaux();
}