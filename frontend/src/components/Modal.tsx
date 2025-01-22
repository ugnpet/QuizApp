import React from "react";

interface ModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSubmit: () => void;
  email: string;
  setEmail: (email: string) => void;
}

const Modal: React.FC<ModalProps> = ({ isOpen, onClose, onSubmit, email, setEmail }) => {
  const isValidEmail = (email: string): boolean => {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(email);
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 bg-gray-900 bg-opacity-50 flex items-center justify-center z-50">
      <div className="bg-white rounded-lg shadow-lg p-6 w-full max-w-md">
        <h2 className="text-xl font-bold text-center mb-4">Enter Your Email</h2>
        <input
          type="email"
          placeholder="Enter your email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          className="w-full p-2 mt-1 border rounded-md focus:ring-blue-500 focus:ring-2 focus:outline-none"
        />
        <div className="mt-4 flex justify-between">
          <button
            onClick={onClose}
            className="bg-gray-300 text-gray-700 py-2 px-4 rounded-lg hover:bg-gray-400"
          >
            Cancel
          </button>
          <button
            onClick={onSubmit}
            disabled={!isValidEmail(email)}
            className={`py-2 px-4 rounded-lg ${
              isValidEmail(email)
                ? "bg-blue-600 text-white hover:bg-blue-700"
                : "bg-blue-300 text-white cursor-not-allowed"
            }`}
          >
            Submit
          </button>
        </div>
      </div>
    </div>
  );
};

export default Modal;
