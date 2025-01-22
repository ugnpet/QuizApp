import React from "react";

interface Props {
  currentStep: number;
  totalQuestions: number;
  onPrev: () => void;
  onNext: () => void;
  onSubmit: () => void;
  areAllAnswered: boolean;
}

const QuizNavigation: React.FC<Props> = ({
  currentStep,
  totalQuestions,
  onPrev,
  onNext,
  onSubmit,
  areAllAnswered,
}) => {
  const isOnLastQuestion = currentStep === totalQuestions - 1;
  const isNextDisabled = isOnLastQuestion && !areAllAnswered;

  return (
    <div className="flex justify-between mb-4 items-center">
      <div className="w-24">
        {currentStep > 0 ? (
          <button onClick={onPrev} className="px-4 py-2 bg-gray-300 rounded-md hover:bg-gray-400">
            Previous
          </button>
        ) : (
          <div className="w-full" />
        )}
      </div>
      <span className="text-xl font-semibold">{`Question ${currentStep + 1} of ${totalQuestions}`}</span>
      <div className="w-24 flex justify-end">
        {isOnLastQuestion ? (
          <button
            onClick={onSubmit}
            className="px-4 py-2 bg-blue-500 rounded-md text-white hover:bg-blue-600 disabled:bg-blue-300"
            disabled={isNextDisabled}
          >
            Submit
          </button>
        ) : (
          <button onClick={onNext} className="px-4 py-2 bg-gray-300 rounded-md hover:bg-gray-400">
            Next
          </button>
        )}
      </div>
    </div>
  );
};

export default QuizNavigation;
