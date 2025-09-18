import { ApiConfig } from "../apiConfig.js";


//ajouter moniteurs

export async function ajouterMoniteur(moniteur) {
  const response = await fetch(`${ApiConfig.baseUrl}/Moniteurs/ajouter`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(moniteur),
  });

  if (!response.ok) {
    const error = await response.text();
    throw new Error(error);
  }


  return await response.json().catch(() => ({}));
}

//lister les moniteurs

export async function listerMoniteurs() {
  const response = await fetch(`${ApiConfig.baseUrl}/Moniteurs/liste`);
  if (!response.ok) throw new Error("Erreur lors de la récupération des moniteurs");
  return await response.json();
}

//supprimer moniteur

export async function supprimerMoniteur(id) {
  const res = await fetch(`${ApiConfig.baseUrl}/Moniteurs/${id}`, {
    method: "DELETE"
  });

  if (!res.ok) {
    const error = await res.text();
    throw new Error(error);
  }
}


//mettre  ajour moniteur

export async function updateActivite(id, nouvelleActivite) {
  const res = await fetch(`${ApiConfig.baseUrl}/Moniteurs/update-activite/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(nouvelleActivite) 
  });

  if (!res.ok) {
    const error = await res.text();
    throw new Error(error);
  }

  return await res.text();
}


