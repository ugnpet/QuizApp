export type QuizType = "radio" | "checkbox" | "text";

export interface Quiz {
  id: number;
  text: string;
  type: QuizType;
  options: string[];
  correctOptions?: string[]; 
}

export interface SubmittedAnswer {
  quizId: number;
  answer: string; 
}

export interface Result {
  userEmail: string;
  score: number;
  timeInSeconds: number;
  answers: SubmittedAnswer[];
}

export interface ResultDTO {
  userEmail: string;
  score: number;
  timeInSeconds: number;
  submittedAt: string;
}
