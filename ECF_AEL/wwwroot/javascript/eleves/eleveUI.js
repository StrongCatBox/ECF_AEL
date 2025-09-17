import { creerEleve } from "./eleveService.js";


// Gestion du formulaire
document.getElementById("eleveForm").addEventListener("submit", async (e) => {
  e.preventDefault();

  const nom = document.getElementById("nomEleve").value;
  const prenom = document.getElementById("prenomEleve").value;
  const dateNaissance = document.getElementById("dateNaissance").value;

  const code =
    document.querySelector('input[name="code"]:checked')?.value === "true";
  const conduite =
    document.querySelector('input[name="permis"]:checked')?.value === "true";

  try {
    await creerEleve(nom, prenom, dateNaissance, code, conduite);
    alert("Élève ajouté avec succès !");
    document.getElementById("eleveForm").reset();
  } catch (err) {
    alert("Erreur : " + err.message);
  }
});
