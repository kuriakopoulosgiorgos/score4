const template = document.createElement('template');
template.innerHTML = `
    <form id="createJoinGameForm" class="col-md-4 offset-md-4" needs-validation novalidate>
        <div class="mb-3">
            <label for="roomName" class="form-label">Room Name</label>
            <input type="text" class="form-control" id="roomName" required>
        </div>
        <button id="createGameSubmitButton" type="submit" class="btn btn-primary">Create Game</button>
        <button id="joinGameSubmitButton" type="submit" class="btn btn-info">Join Game</button>
    </form>
`;

export class CreateJoinGame extends HTMLElement {
    
    #form = null;

    constructor() {
        super();
    }

    connectedCallback() {
        this.render();
    }
    
    render() {
        this.replaceChildren(template.content.cloneNode(true));
        this.#form = document.querySelector('#createJoinGameForm');
        this.#form.addEventListener('submit', this.onValidate.bind(this));
        
        const createGameSubmitButton = document.querySelector('#createGameSubmitButton');
        createGameSubmitButton.addEventListener('click', this.onCreateGame.bind(this));

        const joinGameSubmitButton = document.querySelector('#joinGameSubmitButton');
        joinGameSubmitButton.addEventListener('click', this.onJoinGame.bind(this));
    }

    onValidate(event) {
        event.preventDefault();
        if (!this.#form.checkValidity()) {
            event.stopPropagation()
        }
        this.#form.classList.add('was-validated');
    }
    
    onCreateGame() {
        if (!this.#form.checkValidity()) {
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
    }
    
    onJoinGame() {
        if (!this.#form.checkValidity()) {
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
    }
}

customElements.define('x-create-join-game', CreateJoinGame);
