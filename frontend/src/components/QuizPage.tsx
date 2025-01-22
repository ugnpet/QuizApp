import React, { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { Quiz } from "../types";
import Modal from "../components/Modal";
import QuizNavigation from "./QuizNavigation";
import QuizContent from "./QuizContent";

const QuizPage: React.FC = () => {
  const [questions, setQuestions] = useState<Quiz[]>([]);
  const [answers, setAnswers] = useState<Record<number, string>>({});
  const [email, setEmail] = useState<string>("");
  const [currentStep, setCurrentStep] = useState<number>(0);
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [isErrorModalOpen, setIsErrorModalOpen] = useState<boolean>(false);
  const [errorMessage, setErrorMessage] = useState<string>("");
  const [startTime, setStartTime] = useState<number>(Date.now());
  const [timeInSeconds, setTimeInSeconds] = useState<number>(0);
  const navigate = useNavigate();

  useEffect(() => {
    axios.get("/api/Quiz").then((response) => {
      setQuestions(response.data);
    });
    const timerInterval = setInterval(() => {
      setTimeInSeconds(Math.floor((Date.now() - startTime) / 1000));
    }, 1000);
    return () => clearInterval(timerInterval);
  }, [startTime]);

  const areAllQuestionsAnswered = () => {
    return questions.every((question) => {
      const ans = answers[question.id];
      return typeof ans === "string" && ans.trim() !== "";
    });
  };

  const handleSubmit = () => {
    if (!areAllQuestionsAnswered()) {
      setErrorMessage("Please answer all questions before submitting.");
      setIsErrorModalOpen(true);
      return;
    }
    setIsModalOpen(true);
  };

  const handleFinalSubmit = () => {
    if (!email) {
      setErrorMessage("Please enter your email.");
      setIsErrorModalOpen(true);
      return;
    }
    const result = {
      userEmail: email,
      score: 0,
      timeInSeconds,
      answers: Object.entries(answers).map(([quizId, answer]) => ({
        quizId: parseInt(quizId, 10),
        answer,
      })),
    };
    axios
      .post("/api/results/submit", result)
      .then(() => {
        setIsModalOpen(false);
        navigate("/results");
      })
      .catch((error) => {
        console.error("Error submitting results:", error);
        setErrorMessage("Failed to submit answers. Check the console for details.");
        setIsErrorModalOpen(true);
      });
  };

  const handleNext = () => {
    if (currentStep < questions.length - 1) {
      setCurrentStep((prev) => prev + 1);
    }
  };

  const handlePrev = () => {
    if (currentStep > 0) {
      setCurrentStep((prev) => prev - 1);
    }
  };

  if (!questions || questions.length === 0) {
    return <div>Loading questions...</div>;
  }

  return (
    <div className="min-h-screen bg-gray-50 flex flex-col items-center justify-center p-6">
      <div className="max-w-4xl w-full bg-white rounded-lg shadow-md p-6 relative">
        <div className="absolute top-4 right-4 bg-blue-500 text-white font-semibold py-1 px-3 rounded-lg">
          Time: {timeInSeconds}s
        </div>
        <h1 className="text-3xl font-bold text-center text-blue-600 mb-6">
          Take the Quiz!
        </h1>
        <QuizNavigation
          currentStep={currentStep}
          totalQuestions={questions.length}
          onPrev={handlePrev}
          onNext={handleNext}
          onSubmit={handleSubmit}
          areAllAnswered={areAllQuestionsAnswered()}
        />
        <QuizContent
          question={questions[currentStep]}
          answer={answers[questions[currentStep].id] || ""}
          onAnswerChange={(value) =>
            setAnswers((prev) => ({ ...prev, [questions[currentStep].id]: value }))
          }
          onCheckboxChange={(option: string, checked: boolean) => {
            const selected = answers[questions[currentStep].id]?.split(",") || [];
            const newAnswers = checked
              ? [...selected, option]
              : selected.filter((a) => a !== option);
            setAnswers((prev) => ({
              ...prev,
              [questions[currentStep].id]: newAnswers.join(","),
            }));
          }}
        />
      </div>
      <button
        onClick={() => navigate("/results")}
        className="mt-6 px-6 py-3 bg-gradient-to-r from-green-400 to-blue-500 text-white font-medium text-lg rounded-full shadow-lg transform transition duration-300 hover:scale-105 hover:from-green-500 hover:to-blue-600 focus:outline-none"
      >
        View High Scores
      </button>
      <Modal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        onSubmit={handleFinalSubmit}
        email={email}
        setEmail={setEmail}
      />
    </div>
  );
};

export default QuizPage;
