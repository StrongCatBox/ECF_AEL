import { ajouterEleve, listerEleves } from "./eleveApi.js";

export async function creerEleve(nom, prenom, dateNaissance, code, conduite) {
  if (!nom || !prenom) {
    throw new Error("Nom et prénom sont obligatoires.");
  }
  if (!dateNaissance) {
    throw new Error("Date de naissance est obligatoire.");
  }

  const eleve = {
    nomEleve: nom,
    prenomEleve: prenom,
    dateNaissance: dateNaissance,
    code: code,
    conduite: conduite,
  };

  return await ajouterEleve(eleve);
}

export async function recupererEleves() {
  return await listerEleves();
}
