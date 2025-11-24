const template = document.createElement('template');
template.innerHTML = /*html*/`
    <form id="createPlayerForm" class="col-md-4 offset-md-4" needs-validation novalidate onsubmit="event.preventDefault();">
    <div class="mb-3">
        <label for="playerName" class="form-label">Player Name</label>
        <input type="text" class="form-control" id="playerName" required>
    </div>
    <button id="createPlayerSubmitButton" type="submit" class="btn btn-primary">Submit</button>
    </form>
`;

export class CreatePlayer extends HTMLElement {

    constructor() {
        super();
        this._subscriptions = [];
        this.appendChild(template.content.cloneNode(true));
    }

    connectedCallback() {
        const form = document.querySelector('#createPlayerForm');
        this.addValidation(form);
        const createPlayerSubmitbutton = document.querySelector('#createPlayerSubmitButton');
        createPlayerSubmitbutton
        .addEventListener('click', () => {
            if (!form.checkValidity()) {
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
        });
        this._subscriptions.push(createPlayerSubmitbutton);
    }

    addValidation(form) {
        form.addEventListener('submit', event => {
            if (!form.checkValidity()) {
                event.preventDefault()
                event.stopPropagation()
            }

            form.classList.add('was-validated');
        }, false);
    }

    diconnectedCallback() {
        this._subscriptions.forEach(s => s.replaceWith(s.cloneNode(true)));
    }
}

customElements.define('x-create-player', CreatePlayer);

