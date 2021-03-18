// Add listener to all of the selectable options(answers)
let options = document.querySelectorAll("input");
let questions = document.querySelectorAll("#questions");
let count = 1;
let currentQuestion = document.querySelector('#questionNumber_0');
let seconds = document.getElementById("countdown").textContent;
let selectedAnswer;
let correctIncorrectSpan = document.querySelector(`#checkmark`);

const delay = ms => new Promise(res => setTimeout(res, ms));

let score = {
    isTrue: [],
    categories: [],
};

// When it is the 5th question, send back the data to the server with AJAX
let submitButton = document.querySelector('#submitButton');
submitButton.addEventListener('click', submitQuiz);
function submitQuiz() {
    let category = currentQuestion.querySelector("p").textContent.slice(10);
    const correctAnswer = currentQuestion.querySelector("input[type]").value;
    clearInterval(countDown);

    if (selectedAnswer == correctAnswer) {
        score.isTrue.push(true);
        score.categories.push(category);
    } else {
        score.isTrue.push(false);
        score.categories.push(category);
    }
    $.ajax({
        url: '/Quiz/SubmitQuiz',
        type: "POST",
        data: JSON.stringify(score),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            window.location.href = response.redirectToUrl;
        },
        error: function (response) {
            console.log(response.responseText);
        }
    });
}

// Adding listener for the new game button and if the user confirm that he/she wants
// a new game I added a ajax request to the server -- GET new quiz
let newQuizButton = document.getElementById("newQuizButton");
newQuizButton.addEventListener("click", function () {
    // Reset score object
    if (confirm("Are you sure you want to cancel the current quiz and start a new one?")) {
        $.ajax({
            url: '/Quiz/Index',
            type: "GET",
            contentType: "text/html; charset=utf-8",
            data: {
                // data stuff here
            },
            success: function (res) {
                window.location.href = this.url;
                $("#content").html(res);
            },
            error: function (res) {
                console.log(res);
            }
        });
    }
});

// Adding event listener to the next button
let nextButton = document.getElementById("nextButton");
nextButton.addEventListener("click", async function () {
    checkCurrentQuestion(currentQuestion);
    await delay(2000);
    resetTimer();
    toggleQuestion();
});


function resetTimer() {
    seconds = 45;
}

function toggleQuestion() {
    currentQuestion.style.display = "none";

    count++;

    currentQuestion = currentQuestion.nextElementSibling;

    if (count === 5) {
        nextButton.style.display = "none";
        submitButton.style.display = "block";
    }
    document.querySelector("#countdown").remove();
    correctIncorrectSpan.textContent = "";
    currentQuestion.style.display = "block";
}


function checkCurrentQuestion(currentQuestion) {
    const correctAnswer = currentQuestion.querySelector("input[type]").value;
    escapedCorrectAnswer = unescapeHtml(correctAnswer);

    checkAnswer(currentQuestion, escapedCorrectAnswer);

    score.currentQuestion++;
}

function checkAnswer(currentQuestion, correctAnswer) {
    let radios = currentQuestion.querySelectorAll(`div label.options input`);
    console.log(radios)

    let correctAnswerEl;
    let category = currentQuestion.querySelector("p").textContent.slice(10);

    for (var i = 0; i < radios.length; i++) {
        if (radios[i].value == correctAnswer) {
            correctAnswerEl = radios[i];
        }
        if (radios[i].type === 'radio' && radios[i].checked) {
            // get value, set checked flag or do whatever you need to
            radios[i].setAttribute("name", "selectedAnswer");
            selectedAnswer = radios[i].value;
        }
    }

    if (selectedAnswer == correctAnswer) {
        score.isTrue.push(true);
        score.categories.push(category);
        correctIncorrectSpan.textContent = "✔️";
    } else {
        correctIncorrectSpan.textContent = "❌";
        correctAnswerEl.checked = true;
        var span = correctAnswerEl.nextElementSibling;
        console.log(span.style);
        span.style.background = "#21bf73";
        score.isTrue.push(false);
        score.categories.push(category);
    }
    async () => {
        await delay(3000);
    }}


// Marked the answer as incorrect when 45 seconcs passed
// Have to add it in start of every question
// When an answer is submitted stop the counter
var countDown = setInterval(function () {
    seconds--;
    if (seconds == 0) {
        let correctIncorrectSpan = document.querySelector(`#checkmark`);
        correctIncorrectSpan.textContent = "❌";
        score.isTrue.push(false);
        score.categories.push(); // TODO ADD CATEGORY TO ARRAY
        if (count === 5) {
            submitButton.click();
        }
        nextButton.click();

    }
    document.querySelector("#countdown").textContent = seconds;
}, 1000);

Element.prototype.remove = function () {
    this.parentElement.removeChild(this);
}
NodeList.prototype.remove = HTMLCollection.prototype.remove = function () {
    for (var i = this.length - 1; i >= 0; i--) {
        if (this[i] && this[i].parentElement) {
            this[i].parentElement.removeChild(this[i]);
        }
    }
}

function unescapeHtml(safe) {
    return safe.replace(/&amp;/g, '&')
        .replace(/&lt;/g, '<')
        .replace(/&gt;/g, '>')
        .replace(/&quot;/g, '"')
        .replace(/&#039;/g, "'");
}

