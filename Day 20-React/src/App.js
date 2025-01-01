import logo from "./logo.svg";
import "./App.css";
import LoginForm from "./components/controlledform";
import Sample from "./components/sample";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import UnControlledLoginForm from "./components/uncontrolledcomponent";
import FormWithHook from "./components/formwithhook";
import RegistrationFormWithDynamicFields from "./components/UseFormWithDynamicFields";
import DepartmentList from "./components/demowithoutuseEffect";

function App() {
  return (
    <>
      <Router>
        <Routes>
          
          {/* <Route path="/" element={<LoginForm />} /> */}
          {/* <Route path="/" element={<UnControlledLoginForm/>} /> */}
          {/* <Route path="/" element={<FormWithHook />} /> */}
          {/* <Route path="/" element={<RegistrationFormWithDynamicFields/>} /> */}
          <Route path="/" element={<DepartmentList />} />
          <Route path="/sample" element={<Sample />} />
        </Routes>
      </Router>
      
      <div className="App">
       
      </div>
    </>
  );
}

export default App;
