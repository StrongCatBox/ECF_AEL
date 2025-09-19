import { ApiConfig } from "../apiConfig.js";

// Ajouter une leçon
export async function ajouterLecon(lecon) {
  const res = await fetch(`${ApiConfig.baseUrl}/Lecons/reserver`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(lecon)
  });

  if (!res.ok) {
    const error = await res.text();
    throw new Error(error);
  }
  return await res.json().catch(() => ({}));
}

// Récupérer élèves
export async function listerEleves() {
  const res = await fetch(`${ApiConfig.baseUrl}/Eleves/liste`);
  if (!res.ok) throw new Error("Erreur lors de la récupération des élèves");
  return await res.json();
}

// Récupérer moniteurs
export async function listerMoniteurs() {
  const res = await fetch(`${ApiConfig.baseUrl}/Moniteurs/liste`);
  if (!res.ok) throw new Error("Erreur lors de la récupération des moniteurs");
  return await res.json();
}

// Récupérer modèles de véhicules
export async function listerModelesVehicules() {
  const res = await fetch(`${ApiConfig.baseUrl}/Modeles/liste`);
  if (!res.ok) throw new Error("Erreur lors de la récupération des modèles de véhicules");
  return await res.json();
}

//lister le calendrier

export async function listerCreneaux() {
  const res = await fetch(`${ApiConfig.baseUrl}/Calendrier/liste`);
  if (!res.ok) throw new Error("Erreur lors de la récupération des créneaux");
  return await res.json();
}

