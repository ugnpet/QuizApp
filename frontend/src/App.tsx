import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import QuizPage from "./components/QuizPage";
import ResultPage from "./components/ResultPage";
import "./App.css";

const App: React.FC = () => {
  return (
    <Router>
      <div className="App">
        <Routes>
          <Route path="/" element={<QuizPage />} />
          <Route path="/results" element={<ResultPage />} />
        </Routes>
      </div>
    </Router>
  );
};

export default App;
