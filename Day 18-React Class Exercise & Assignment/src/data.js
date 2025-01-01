import reactComponent from "../src/assets/react-components.jpeg";
import reactCoreConcept from "../src/assets/react-core-concepts.png";
import reactProperty from "../src/assets/react-props.jpeg";
import reactJsx from "../src/assets/reaact-jsx.jpeg";

export const CORE_CONCEPTS = [
  {
    image: reactComponent,
    title: "Components",
    description:
      "React Components are the building blocks of ReactJS application. They help to break the user interface into smaller, reusable chunks, making the code easier to manage and maintain. Components can be class-based or function-based, and each type plays an important role in building dynamic and interactive web applicatio",
  },
  {
    image: reactProperty,
    title: "Props",
    description:
      "In ReactJS, the data can be passed from one component to another component using these props, similar to how the arguments are passed in a function.",
  },
  {
    image: reactCoreConcept,
    title: "CoreConcepts",
    description: `The building blocks of a React application's UI, components can be as simple as a button or as complex as an entire app. Components are reusable and can be defined and combined to form larger components.`,
  },
  {
    image: reactJsx,
    title: "JSX",
    description:
      "JSX stands for JavaScript XML. JSX allows us to write HTML in React. JSX makes it easier to write and add HTML in React",
  },
];