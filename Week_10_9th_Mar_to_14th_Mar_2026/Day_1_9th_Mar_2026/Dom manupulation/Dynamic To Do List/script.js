let input = document.getElementById("taskInput");
let addBtn = document.getElementById("addBtn");
let taskList = document.getElementById("taskList");

addBtn.addEventListener("click", function() {
    let taskText = input.value.trim();

    if (taskText !== "") {
        let li = document.createElement("li");
        li.textContent = taskText;

        li.addEventListener("click", function() {
            li.classList.toggle("done");
        });

        taskList.appendChild(li);

        input.value = "";
    }
});
