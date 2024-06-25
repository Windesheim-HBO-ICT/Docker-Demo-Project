// Add item to the server
function addItem() {
    if($('#newTask').val() == '') {
        return;
    }

    fetch('http://localhost:8080/api/Items', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { 
                name: $('#newTask').val(),
                isChecked: false,
            }
        )
    })
    .then(response => response.json())
    .then(data => {
        console.log('Item added:', data);
        window.location.reload();
    })
    .catch(error => console.error('Error adding item:', error));
}

// Update item on the server
function updateItem(id) {
    fetch(`http://localhost:8080/api/Items/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            {
                id: id
            }
        )
    })
    .then(response => response.json())
    .then(data => {
        console.log('Item updated:', data);
        window.location.reload();
    })
    .catch(error => console.error('Error updating item:', error));
}

// Delete item from the server
function deleteItem(id) {
    fetch(`http://localhost:8080/api/Items/${id}`, {
        method: 'DELETE'
    })
    .then(response => response.json())
    .then(data => {
        console.log('Item deleted:', data);
        window.location.reload();
    })
    .catch(error => console.error('Error deleting item:', error));
}

$(document).ready(function() {
    "use strict";

    // Fetch items from the server
    var fetchItems = function() {
        fetch('http://localhost:8080/api/Items')
            .then(response => response.json())
            .then(data => {
                data.forEach(item => {
                    console.log(item.id, item.name, item.isChecked)
                    const todoItemHtml = `<div class="todo-item d-flex align-items-center" id=${item.id}><div class="checker"><span class=""><input type="checkbox" ${item.isChecked ? "checked" : ""} onclick="updateItem(${item.id})"></span></div> <span class="ml-4">${item.name}</span> <p onclick="deleteItem(${item.id})" class="ml-auto remove-todo-item"><svg fill="#000000" width="28px" height="28px" viewBox="-4.8 -4.8 41.60 41.60" version="1.1" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <title>remove</title> <path d="M11.188 4.781c6.188 0 11.219 5.031 11.219 11.219s-5.031 11.188-11.219 11.188-11.188-5-11.188-11.188 5-11.219 11.188-11.219zM11.25 17.625l3.563 3.594c0.438 0.438 1.156 0.438 1.594 0 0.406-0.406 0.406-1.125 0-1.563l-3.563-3.594 3.563-3.594c0.406-0.438 0.406-1.156 0-1.563-0.438-0.438-1.156-0.438-1.594 0l-3.563 3.594-3.563-3.594c-0.438-0.438-1.156-0.438-1.594 0-0.406 0.406-0.406 1.125 0 1.563l3.563 3.594-3.563 3.594c-0.406 0.438-0.406 1.156 0 1.563 0.438 0.438 1.156 0.438 1.594 0z"></path> </g></svg></p></p></div>`;
                    $('.todo-list').append(todoItemHtml);
                });
            })
            .catch(error => console.error('Error fetching data:', error));
    };
    
    fetchItems();
}); 