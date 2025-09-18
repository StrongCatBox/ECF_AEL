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

  document.getElementById("eleveToast").classList.remove("bg-danger");
  document.getElementById("eleveToast").classList.add("bg-success");
  document.getElementById("eleveToastMessage").textContent = "Élève ajouté avec succès !";

  const toast = new bootstrap.Toast(document.getElementById("eleveToast"));
  toast.show();

  document.getElementById("eleveForm").reset();
} catch (err) {
  document.getElementById("eleveToast").classList.remove("bg-success");
  document.getElementById("eleveToast").classList.add("bg-danger");
  document.getElementById("eleveToastMessage").textContent = "Erreur : " + err.message;

  const toast = new bootstrap.Toast(document.getElementById("eleveToast"));
  toast.show();
}

});
