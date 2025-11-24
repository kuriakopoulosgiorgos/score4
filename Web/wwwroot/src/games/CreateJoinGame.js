const template = document.createElement('template');
template.innerHTML = /*html*/`
    <form id="createJoinGameForm" class="col-md-4 offset-md-4" needs-validation novalidate onsubmit="event.preventDefault();">
    <div class="mb-3">
        <label for="roomName" class="form-label">Room Name</label>
        <input type="text" class="form-control" id="roomName" required>
    </div>
    <button id="createGameSubmitButton" type="submit" class="btn btn-primary">Create Game</button>
    <button id="joinGameSubmitButton" type="submit" class="btn btn-info">Join Game</button>
    </form>
`;

export class CreateJoinGame extends HTMLElement {

    constructor() {
        super();
        this._subscriptions = [];
        this.appendChild(template.content.cloneNode(true));
    }

    connectedCallback() {
        const form = document.querySelector('#createJoinGameForm');
        this.addValidation(form);
        const createGameSubmitButton = document.querySelector('#createGameSubmitButton');
        createGameSubmitButton
        .addEventListener('click', () => {
            if (!form.checkValidity()) {
                return;
            }
            const roomName = document.querySelector('#roomName').value;
            this.dispatchEvent(
              new CustomEvent('onCreateGame', {
                detail: { roomName: roomName },
                bubbles: true,
                composed: true
              })
            );
        });
        const joinGameSubmitButton = document.querySelector('#joinGameSubmitButton');
        joinGameSubmitButton
        .addEventListener('click', () => {
            if (!form.checkValidity()) {
                return;
            }
            const roomName = document.querySelector('#roomName').value;
            this.dispatchEvent(
              new CustomEvent('onJoinGame', {
                detail: { roomName: roomName },
                bubbles: true,
                composed: true
              })
            );
        });
        this._subscriptions.push(createGameSubmitButton);
        this._subscriptions.push(joinGameSubmitButton);
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

customElements.define('x-create-join-game', CreateJoinGame);
