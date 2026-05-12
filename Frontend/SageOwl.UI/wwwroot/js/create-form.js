let questionIndex = 0;

$(document).on("click", ".add_option", function (e) {
    e.preventDefault();

    const $card = $(this).closest(".form_create_card");
    const $optionsContainer = $card.find(".optionsContainer");

    const currentQuestionIndex = $(".form_create_card").index($card);

    const optionIndex = $optionsContainer.children(".form_option").length;
    const optionLabel = String.fromCharCode(65 + optionIndex);

    const newOption = `
                    <div class="form_option"
                        style="display:flex;flex-direction:row;align-items:center;gap:10px;">

                        <label style="font-weight:bold;font-size:16px;">
                            ${optionLabel}.
                        </label>
                        
                        <input type="text"
                           style="border:none;height:40px;padding-left:10px;outline:none;flex:1;"
                           name="Questions[${questionIndex}].Options[${optionIndex}].Description"
                           placeholder="Option Description" />

                        <button class="remove_option" type="button">
                            Remove
                        </button>
                    </div>`;

    $optionsContainer.append(newOption);
});

$("#add_question").on("click", function (e) {
    e.preventDefault();

    questionIndex++;

    const $formCard = $(".form_create_card").first();
    const $newCard = $formCard.clone(true);

    $newCard.find("input[type=text]").val("");
    $newCard.find(".optionsContainer").empty();

    $newCard.find("input, select, textarea").each(function () {

        let nameAttr = $(this).attr("name");

        if (nameAttr) {
            let newName = nameAttr.replace(
                /\[\d+\]/,
                `[${questionIndex}]`);
            $(this).attr("name", newName);
        }
    });

    $("#form_container").append($newCard);
});

$(document).on("click", ".remove_question", function (e) {
    e.preventDefault();

    const totalCards = $(".form_create_card").length;

    if (totalCards === 1) {
        return;
    }

    $(this).closest(".form_create_card").remove();

    reindexQuestions();
});

$(document).on("click", ".remove_option", function (e) {
    e.preventDefault();

    const $card = $(this).closest(".form_create_card");
    const $option = $(this).closest(".form_option");

    $option.remove();

    reindexOptions($card);
});

$(document).on("change", "input[name='type']", function () {

    const selectedType = $(this).val();

    const $card = $(this).closest(".form_create_card");

    const $optionsContainer = $card.find(".optionsContainer");

    const $addOptionButton = $card.find(".add_option");

    if (selectedType === "opened_ended") {

        $optionsContainer.empty();

        $addOptionButton.prop("disabled", true);

        $addOptionButton.css({
            opacity: "0.5",
            cursor: "not-allowed"
        });
    }
    else {
        $addOptionButton.prop("disabled", false);

        $addOptionButton.css({
            opacity: "1",
            cursor: "pointer"
        });
    }
});

function reindexQuestions() {

    $(".form_create_card").each(function (questionIdx) {

        $(this).find("input, select, textarea").each(function () {

            let nameAttr = $(this).attr("name");

            if (nameAttr) {

                let newName = nameAttr.replace(
                    /Questions\[\d+\]/,
                    `Questions[${questionIdx}]`
                );

                $(this).attr("name", newName);
            }
        });

        $(this).find(".form_option").each(function (optionIdx) {

            const optionLabel = String.fromCharCode(65 + optionIdx);

            $(this).find("label").text(`${optionLabel}.`);

            $(this).find("input").attr(
                "name",
                `Questions[${questionIdx}].Options[${optionIdx}].Description`
            );
        });
    });

    questionIndex = $(".form_create_card").length - 1;
}

function reindexOptions($card) {

    const questionIdx = $(".form_create_card").index($card);

    $card.find(".form_option").each(function (optionIdx) {

        const optionLabel = String.fromCharCode(65 + optionIdx);

        $(this).find("label").text(`${optionLabel}.`);

        $(this).find("input[type='text']").attr(
            "name",
            `Questions[${questionIdx}].Options[${optionIdx}].Description`
        );
    });
}