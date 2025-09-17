import { ApiConfig } from "../apiConfig.js";

export async function ajouterEleve(eleve) {
  const response = await fetch(`${ApiConfig.baseUrl}/Eleves/ajouter`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(eleve),
  });

  if (!response.ok) {
    const error = await response.text();
    throw new Error(error);
  }

  // si le back ne renvoie rien -> éviter une erreur de parsing
  return await response.json().catch(() => ({}));
}

export async function listerEleves() {
  const response = await fetch(`${ApiConfig.baseUrl}/Eleves/liste`);
  if (!response.ok) throw new Error("Erreur lors de la récupération des élèves");
  return await response.json();
}
