import { ApiConfig } from "../apiConfig.js";


//ajouter vehicules

export async function ajouterVehicule(vehicule) {
  const response = await fetch(`${ApiConfig.baseUrl}/Vehicules/ajouter`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(vehicule),
  });

  if (!response.ok) {
    const error = await response.text();
    throw new Error(error);
  }


  return await response.json().catch(() => ({}));
}

//lister les vehicules

export async function listerVehicules() {
  const response = await fetch(`${ApiConfig.baseUrl}/Vehicules/liste`);
  if (!response.ok) throw new Error("Erreur lors de la récupération des vehicules");
  return await response.json();
}

//supprimer vehicule

export async function supprimerVehicule(id) {
  const res = await fetch(`${ApiConfig.baseUrl}/Vehicules/${immatric}`, {
    method: "DELETE"
  });

  if (!res.ok) {
    const error = await res.text();
    throw new Error(error);
  }
}


//mettre  ajour vehicule

export async function updateEtatVehicule(immatric, nouvelEtat) {
  const res = await fetch(`${ApiConfig.baseUrl}/Vehicules/update-etat/${immatric}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(nouvelEtat) 
  });

  if (!res.ok) {
    const error = await res.text();
    throw new Error(error);
  }

  return await res.text();
}


