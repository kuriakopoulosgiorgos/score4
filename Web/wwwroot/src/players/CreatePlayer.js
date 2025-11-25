const template = document.createElement('template');
template.innerHTML = `
    <form id="createPlayerForm" class="col-md-4 offset-md-4" needs-validation novalidate>
        <div class="mb-3">
            <label for="playerName" class="form-label">Player Name</label>
            <input type="text" class="form-control" id="playerName" required>
        </div>
        <button id="createPlayerSubmitButton" type="submit" class="btn btn-primary">Submit</button>
    </form>
`;

export class CreatePlayer extends HTMLElement {

    #form = null;
    constructor() {
        super();
    }

    connectedCallback() {
        this.render();
    }
    
    render() {
        this.replaceChildren(template.content.cloneNode(true));
        this.#form = document.querySelector('#createPlayerForm');
        this.#form.addEventListener('submit', this.onValidate.bind(this));
        const createPlayerSubmitButton = document.querySelector('#createPlayerSubmitButton');
        createPlayerSubmitButton.addEventListener('click', this.onPlayerSubmit.bind(this));
    }

    onValidate(event) {
        event.preventDefault();
        if (!this.#form.checkValidity()) {
            event.stopPropagation();
        }
        this.#form.classList.add('was-validated');
    }
    
    onPlayerSubmit() {
        if (!this.#form.checkValidity()) {
            return;
        }
        const playerName = document.querySelector('#playerName').value;
        this.dispatchEvent(
            new CustomEvent('onPlayerCreate', {
                detail: { playerName: playerName },
                bubbles: true,
                composed: true
            })
        );
    }
}

customElements.define('x-create-player', CreatePlayer);
