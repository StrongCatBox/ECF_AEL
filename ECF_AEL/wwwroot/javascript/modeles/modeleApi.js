import { ApiConfig } from "../apiConfig.js";

export async function ajouterModele(modele) {
  const res = await fetch(`${ApiConfig.baseUrl}/Modeles/ajouter`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(modele)
  });

  if (!res.ok) {
    const error = await res.text();
    throw new Error(error);
  }
  return await res.json().catch(() => ({}));
}

export async function listerModeles() {
  const res = await fetch(`${ApiConfig.baseUrl}/Modeles/liste`);
  if (!res.ok) throw new Error("Erreur lors de la récupération des modèles");
  return await res.json();
}
