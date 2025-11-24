const template = document.createElement('template');
template.innerHTML = /*html*/`
    <h1>Board</h1>
`;

export class Board extends HTMLElement {

    constructor() {
        super();
    }

    connectedCallback() {
        this.appendChild(template.content.cloneNode(true));

    }

    diconnectedCallback() {
    }
}

customElements.define('x-board', Board);
