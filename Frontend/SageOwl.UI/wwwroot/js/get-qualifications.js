async function loadProduct(id) {

    const response = await fetch(`/qualification/QualificationPartial?id=${id}`);

    const html = await response.text();

    document.getElementById("qualificationContainer").innerHTML = html;
}

function search() {

    const id =
        document.getElementById("periodSelect").value;

    loadProduct(id);
}