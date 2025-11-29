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
            border-radius: 50%;
            display: inline-block;
        }
        
        .white {
            background-color: white;
        }
        
        .blue {
            background-color: rgba(84,84,232,0.95);
        }
        
        .red {
            background-color: rgba(250,64,64,0.98);
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
        const color = this.getAttribute('color');
        this.shadowRoot.querySelector('.circle').classList.add(color);
    }
}

customElements.define('x-cell', Cell);
