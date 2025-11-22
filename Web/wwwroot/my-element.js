import {html, css, LitElement} from 'https://cdn.jsdelivr.net/gh/lit/dist@3/core/lit-core.min.js';

export class MyElement extends LitElement {
    static properties = {
        greeting: {},
        planet: {},
    };

    static styles = css`
    :host {
      display: inline-block;
      padding: 10px;
      background: lightgray;
    }
    .planet {
      color: blue;
    }
  `;

    constructor() {
        super();
        this.greeting = 'Hello';
        this.planet = 'World';
    }

    render() {
        return html`
      <span @click=${this.togglePlanet}
        >${this.greeting}
        <span class="planet">${this.planet}</span>
      </span>
    `;
    }

    togglePlanet() {
        this.planet = this.planet === 'World' ? 'Mars' : 'World';
    }
}
customElements.define('my-element', MyElement);
