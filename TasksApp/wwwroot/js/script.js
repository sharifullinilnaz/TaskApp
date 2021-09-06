function check() {
	let isActive = false;
	const nodes = document.querySelectorAll("#checkable")

	for (let i = 0; i < nodes.length; i++) {
		nodes[i].addEventListener('click', function () {
			if (!isActive && this.style.backgroundColor !== 'gray') {
				this.style.backgroundColor = 'gray'
				document.getElementById("modal_edit").disabled = false;
				document.getElementById("modal_delete").disabled = false;
				isActive = true;
				selectedId = this.querySelector('.table__item_id').value;
				selectedName = this.querySelector('.table__item_name').textContent.trim();
				selectedDescription = this.querySelector('.table__item_description').textContent.trim();
			}
			else if (isActive && this.style.backgroundColor === 'gray') {
				this.style.backgroundColor = 'white'
				document.getElementById("modal_edit").disabled = true;
				document.getElementById("modal_delete").disabled = true;
				isActive = false;
			}
		})
	}
}


//open modal window

document.getElementById("modal_open").onclick = () => {
	modal.classList.toggle("modal__open")
}


document.getElementById("modal_edit").onclick = () => {
	modal_editing.classList.toggle("modal__open")
	document.getElementById('task_update_name').value = `${selectedName}`;
	document.getElementById('task_update_description').value = `${selectedDescription}`;
}

//close modal window
document.querySelector("#modal").onclick = () => {
	modal.classList.remove("modal__open")
}

document.querySelector("#modal_editing").onclick = () => {
	modal_editing.classList.remove("modal__open")
}

document.querySelector(".modal__content_button_cancel").onclick = () => {
	modal.classList.remove("modal__open")
}

document.querySelector(".modal__content_editing").onclick = (event) => {
	event.stopPropagation()
}

document.querySelector(".modal__content").onclick = (event) => {
	event.stopPropagation()
}


document.querySelector(".modal__content_button_cancel_edit").onclick = () => {
	modal_editing.classList.remove("modal__open")
}

const uri = 'api/Task';
const uriStatus = 'api/Status';
let tasks = [];
let statuses = [];
var selectedId = "";
var selectedName = "";
var selectedDescription = "";

function getTasks() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayTasks(data))
        .catch(error => console.error('Unable to get tasks.', error));
}

function getStatuses() {
	fetch(uriStatus)
		.then(response => response.json())
		.then(data => fillStatuses(data.data))
		.catch(error => console.error('Unable to get statuses.', error));
}

function fillStatuses(arr) {
	statuses = arr;
	_displayStatuses(arr);
}


function _displayTasks(data) {
	const tBody = document.getElementById('tasks');
	tBody.innerHTML = '';
	data.data.forEach(item => {
		tBody.insertAdjacentHTML("beforeend", `
						<div class="table__item" id="checkable">
							<div class="table__item_name">
								${item.name}
							</div>
							<div class="table__item_description">
								${item.description}
							</div>
							<div class="table__item_status">
								${item.status.statusName}
							</div>
							<input class="table__item_id" value="${item.id}" hidden>
						</div>`);
	});

	tasks = data;
	check();
}

function _displayStatuses(data) {
	const selectBody = document.getElementById('status_option_items');
	data.forEach(item => {
		selectBody.insertAdjacentHTML("beforeend", `<option value="${item.statusId}">${item.statusName}</option>`);
	});
	const selectBodyUpdate = document.getElementById('status_option_items_update');
	data.forEach(item => {
		selectBodyUpdate.insertAdjacentHTML("beforeend", `<option value="${item.statusId}">${item.statusName}</option>`);
	});
}

function createTask() {

	const name = document.getElementById('task_add_name');
	const description = document.getElementById('task_add_description');
	const statusOPtions = document.getElementById('status_option_items');

	const task = {
		name: name.value.trim(),
		description: description.value.trim(),
		statusId: statusOPtions.options[statusOPtions.selectedIndex].value
	};

	fetch(uri, {
		method: 'POST',
		headers: {
			'Accept': 'application/json',
			'Content-Type': 'application/json'
		},
		body: JSON.stringify(task)
	})
		.then(response => response.json())
		.then(() => {
			getTasks();
		})
		.catch(error => console.error('Unable to create task.', error));
	name.value = "";
	description.value = "";
	modal.classList.remove("modal__open");
}

function deleteTask() {
	fetch(`${uri}/delete/${selectedId}`, {
		method: 'DELETE'
	})
		.then(() => getTasks())
		.catch(error => console.error('Unable to delete task.', error));
}

function updateTask() {

	const name = document.getElementById('task_update_name');
	const description = document.getElementById('task_update_description');
	const statusOPtions = document.getElementById('status_option_items_update');

	const task = {
		id: selectedId,
		name: name.value.trim(),
		description: description.value.trim(),
		statusId: statusOPtions.options[statusOPtions.selectedIndex].value
	};

	fetch(uri, {
		method: 'PUT',
		headers: {
			'Accept': 'application/json',
			'Content-Type': 'application/json'
		},
		body: JSON.stringify(task)
	})
		.then(response => response.json())
		.then(() => getTasks())
		.catch (error => console.error('Unable to update task.', error));
	modal_editing.classList.remove("modal__open")
}
