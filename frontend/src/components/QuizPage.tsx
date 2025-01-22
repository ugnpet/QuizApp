import React, { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { Quiz, SubmittedAnswer } from "../types";

const QuizPage: React.FC = () => {
  const [questions, setQuestions] = useState<Quiz[]>([]);
  const [answers, setAnswers] = useState<Record<number, string>>({});
  const [email, setEmail] = useState<string>("");
  const navigate = useNavigate();

  // Fetch questions using the proxy
  useEffect(() => {
    axios.get("/api/Quiz").then((response) => {
      setQuestions(response.data);
    });
  }, []);

  // Handle form submission
  const handleSubmit = () => {
    if (!email) {
      alert("Please enter your email.");
      return;
    }

    // Construct the result payload
    const result = {
      userEmail: email,
      score: 0, // Score is calculated in the backend
      timeInSeconds: 0, // Placeholder for now
      answers: Object.entries(answers).map(([quizId, answer]) => ({
        quizId: parseInt(quizId, 10), // Ensure quizId is parsed as an integer
        answer,
      })),
    };

    axios
      .post("/api/results/submit", result)
      .then(() => {
        navigate("/results");
      })
      .catch((error) => {
        console.error("Error submitting results:", error);
        alert("Failed to submit answers. Check the console for details.");
      });
  };

  // Render inputs based on question type
  const renderQuestionInput = (question: Quiz) => {
    switch (question.type.toLowerCase()) {
      case "radio":
        return question.options.map((option) => (
          <div key={option}>
            <input
              type="radio"
              name={`question-${question.id}`}
              value={option}
              onChange={(e) =>
                setAnswers({ ...answers, [question.id]: e.target.value })
              }
            />
            <label>{option}</label>
          </div>
        ));
      case "checkbox":
        return question.options.map((option) => (
          <div key={option}>
            <input
              type="checkbox"
              value={option}
              onChange={(e) => {
                const selected = answers[question.id]?.split(",") || [];
                const newAnswers = e.target.checked
                  ? [...selected, option]
                  : selected.filter((a) => a !== option);

                setAnswers({ ...answers, [question.id]: newAnswers.join(",") });
              }}
            />
            <label>{option}</label>
          </div>
        ));
      case "text":
        return (
          <input
            type="text"
            onChange={(e) =>
              setAnswers({ ...answers, [question.id]: e.target.value })
            }
          />
        );
      default:
        return null;
    }
  };

  return (
    <div>
      <h1>Quiz</h1>
      <input
        type="email"
        placeholder="Enter your email"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        required
      />
      {questions.map((question) => (
        <div key={question.id}>
          <h3>{question.text}</h3>
          {renderQuestionInput(question)}
        </div>
      ))}
      <button onClick={handleSubmit}>Submit</button>
    </div>
  );
};

export default QuizPage;
