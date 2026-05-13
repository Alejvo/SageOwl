let questionIndex = 0;

$(document).on("click", ".add_option", function (e) {
    e.preventDefault();

    if ($(this).prop("disabled") || $(this).css("cursor") === "not-allowed") {
        return false;
    }

    const $card = $(this).closest(".form_create_card");
    const $optionsContainer = $card.find(".optionsContainer");
    const currentQuestionIndex = $(".form_create_card").index($card);
    const optionIndex = $optionsContainer.children(".form_option").length;
    const optionLabel = String.fromCharCode(65 + optionIndex);

    const newOption = `
                    <div class="form_option"
                        style="display:flex;flex-direction:row;align-items:center;gap:10px;">

                        <label style="font-weight:bold;font-size:16px;"
                               name="NewForm.Questions[${currentQuestionIndex}].Options[${optionIndex}].Value">
                            ${optionLabel}.
                        </label>
                        
                        <input type="text"
                           style="border:none;height:40px;padding-left:10px;outline:none;flex:1;"
                           name="NewForm.Questions[${currentQuestionIndex}].Options[${optionIndex}].Value"
                           placeholder="Option Description" />

                        <input type="radio"
                           name="NewForm.Questions[${currentQuestionIndex}].Options[${optionIndex}].IsCorrect"
                           value="true"
                        />
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
    const $newCard = $formCard.clone();

    $newCard.find("input[type=text]").val("");
    $newCard.find(".optionsContainer").empty();

    const $addOptionBtn = $newCard.find(".add_option");
    $addOptionBtn.prop("disabled", false).css({
        opacity: "1",
        cursor: "pointer"
    });

    $newCard.find(".question_type").prop("checked", false);
    $newCard.find(".question_type[value='closed']").prop("checked", true);

    $newCard.find("input, select, textarea").each(function () {
        let nameAttr = $(this).attr("name");
        if (nameAttr) {
            let newName = nameAttr.replace(
                /NewForm\.Questions\[\d+\]/,
                `NewForm.Questions[${questionIndex}]`);

            $(this).attr("name", newName);
        }

        let idAttr = $(this).attr("id");
        if (idAttr) {

            let newId = idAttr.replace(
                /NewForm_Questions_\d+/,
                `NewForm_Questions_${questionIndex}`
            );

            $(this).attr("id", newId);
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

$(document).on("change", ".question_type", function () {
    const $card = $(this).closest(".form_create_card");

    const $selectedType = $card.find(".question_type:checked").val();

    const $optionsContainer = $card.find(".optionsContainer");
    const $addOptionButton = $card.find(".add_option");

    if ($selectedType === "opened") {

        $optionsContainer.empty();

        $addOptionButton
            .prop("disabled", true)
            .css({
                opacity: "0.5",
                cursor: "not-allowed"
            });
    }
    else {
        $addOptionButton
            .prop("disabled", false)
            .css({
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
                    /NewForm\.Questions\[\d+\]/,
                    `NewForm.Questions[${questionIdx}]`
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

        $(this).find("input[type='radio']").attr(
            "name",
            `Questions[${questionIdx}].Options[${optionIdx}].IsCorrect`
        );
    });
}