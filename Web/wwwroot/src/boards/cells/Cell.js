const template = document.createElement('template');
template.innerHTML = `
    <style>
        .cell {
            background-color: burlywood;
            padding: 0.3em;
        }
    
        .circle {
            aspect-ratio: 1 / 1;
            width: 100%;
            max-height: 100%;
            background-color: white;
            border-radius: 50%;
            display: inline-block;
        }
    </style>
    
    <div class="cell">
        <span class="circle"></span>
    </div>
`;

export class Cell extends HTMLElement {

    constructor() {
        super();
        this.attachShadow({mode: 'open'});
        this.render();
    }
    
    render() {
        this.shadowRoot.replaceChildren(template.content.cloneNode(true));
    }
}

customElements.define('x-cell', Cell);
