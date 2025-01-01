import logo from "./logo.svg";
import "./App.css";
import conceptImg from "../src/assets/react-core-concepts.png";
import Profile from "./components/avatar";
import CoreConcept from "./components/CoreConcepts";  
import reactComponent from "./assets/react-components.jpeg";
import { CORE_CONCEPTS } from "./data";
import CssClassDemo from "./css/cssClassDemo";
import Counter from "./components/counter";
import InlineStyleExample from "./css/inlineStyle";
import LoginLogout from "./components/loginlogoutdemo";
import ServerStatus from "./components/serverwithstatus";
import ProductStatus from "./components/ProductStatus";

const reactDescriptions = ["Fundamental", "Intermediate", "Advanced"];

function getRandomInt(max) {
  return Math.floor(Math.random() * (max + 1));
}

function Header() {
  const description = reactDescriptions[getRandomInt(2)];
  return (
    <header>
      <main>
        <section>
        <h1>Product Delivery Status</h1>
        <ProductStatus />
        <h1>Server Status Example</h1>
        <ServerStatus />
        <h1>Login and Logout Example</h1>
        <LoginLogout />
        <h1>InLine Style Css Example</h1>
        <InlineStyleExample />
        <h1>External Style Css Example</h1>
        <CssClassDemo />
        <h1>React Counter Example</h1>
        <Counter />
          <h2>Core concepts</h2>
          <ul>
            <CoreConcept
              title={CORE_CONCEPTS[0].title}
              image={CORE_CONCEPTS[0].image}
              description={CORE_CONCEPTS[0].description}
            />
            <CoreConcept
              title={CORE_CONCEPTS[1].title}
              image={CORE_CONCEPTS[1].image}
              description={CORE_CONCEPTS[1].description}
            />
            <CoreConcept {...CORE_CONCEPTS[2]} />
            <CoreConcept {...CORE_CONCEPTS[3]} />
          </ul>
        </section>
      </main>
    </header>
  );
}

function App() {
  return (
    <div className="App">
      <Header />
      <main>Time to start Conditional Rendering concepts </main>
    </div>
  );
}

export default App;


