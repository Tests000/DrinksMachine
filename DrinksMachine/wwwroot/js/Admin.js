let editor = document.createElement('div');
editor.className = "edit";
let form = document.createElement('form');
form.setAttribute('method', 'post');


let inputImage = document.createElement('input');
inputImage.setAttribute('type', 'file');
inputImage.setAttribute('name', 'Image');
let divImage = document.createElement('div');
divImage.appendChild(inputImage);
form.appendChild(divImage);

let labelName = document.createElement('label');
labelName.setAttribute('for', 'Name')
labelName.textContent = "Name";
let inputName = document.createElement('input');
inputName.setAttribute('type', 'text');
inputName.setAttribute('name', 'Name');
let divName = document.createElement('div');
divName.appendChild(labelName);
divName.appendChild(inputName);
form.appendChild(divName);

let labelCost = document.createElement('label');
labelCost.setAttribute('for', 'Cost')
labelCost.textContent = "Cost";
let inputCost = document.createElement('input');
inputCost.setAttribute('type', 'number');
inputCost.setAttribute('min', '0');
inputCost.setAttribute('name', 'Cost');
let divCost = document.createElement('div');
divCost.appendChild(labelCost);
divCost.appendChild(inputCost);
form.appendChild(divCost);

let labelCount = document.createElement('label');
labelCount.setAttribute('for', 'Count')
labelCount.textContent = "Count";
let inputCount = document.createElement('input');
inputCount.setAttribute('type', 'number');
inputCount.setAttribute('min', '0');
inputCount.setAttribute('name', 'Count');
let divCount = document.createElement('div');
divCount.appendChild(labelCount);
divCount.appendChild(inputCount);
form.appendChild(divCount);

let button = document.createElement('button');
button.textContent = "Save";
button.setAttribute('name', 'objId');
form.appendChild(button);
form.setAttribute('method', 'post');
form.setAttribute('enctype', 'multipart/form-data');

editor.appendChild(form);

function Edit(e, id) {
    let hideList = document.getElementsByClassName('hideElem');
    editor.querySelector('button').value = id;

    for (let i = 0; i < hideList.length;) {
        hideList[i].className = '';
    }
    if (id != -1) {
        e = e.parentNode;
        e = e.parentNode;
        for (let i = 0; i < e.childNodes.length; i++) {
            let child = e.childNodes[i];
            child.className = "hideElem";
        }
    }
    else {

        let contain = e.parentNode;
        e.className = "hideElem";
        e = contain;
    }
    e.appendChild(editor);
}