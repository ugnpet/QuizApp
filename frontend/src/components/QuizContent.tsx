import React from "react";
import { Quiz } from "../types";

interface Props {
  question: Quiz;
  answer: string;
  onAnswerChange: (value: string) => void;
  onCheckboxChange: (option: string, checked: boolean) => void;
}

const QuizContent: React.FC<Props> = ({ question, answer, onAnswerChange, onCheckboxChange }) => {
  const renderQuestionInput = () => {
    switch (question.type.toLowerCase()) {
      case "radio":
        return question.options.map((option) => (
          <div key={option} className="flex items-center gap-2">
            <input
              type="radio"
              name={`question-${question.id}`}
              value={option}
              checked={answer === option}
              className="w-5 h-5 text-blue-600 focus:ring-blue-500 focus:ring-2"
              onChange={(e) => onAnswerChange(e.target.value)}
            />
            <label className="text-gray-700">{option}</label>
          </div>
        ));
      case "checkbox":
        return question.options.map((option) => (
          <div key={option} className="flex items-center gap-2">
            <input
              type="checkbox"
              checked={answer.split(",").includes(option)}
              value={option}
              className="w-5 h-5 text-green-600 focus:ring-green-500 focus:ring-2"
              onChange={(e) => onCheckboxChange(option, e.target.checked)}
            />
            <label className="text-gray-700">{option}</label>
          </div>
        ));
      case "text":
        return (
          <input
            type="text"
            className="w-full p-2 mt-1 border rounded-md focus:ring-blue-500 focus:ring-2 focus:outline-none"
            value={answer}
            onChange={(e) => onAnswerChange(e.target.value)}
          />
        );
      default:
        return null;
    }
  };

  return (
    <div className="space-y-6">
      <div key={question.id} className="p-4 border rounded-lg">
        <h3 className="text-lg font-semibold text-gray-800">{question.text}</h3>
        <div className="mt-2">{renderQuestionInput()}</div>
      </div>
    </div>
  );
};

export default QuizContent;
