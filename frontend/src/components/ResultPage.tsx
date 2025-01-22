import React, { useState, useEffect } from "react";
import axios from "axios";
import { List, ListItem, ListItemText, Typography } from "@mui/material";
import { ResultDTO } from "../types";

const ResultPage: React.FC = () => {
  const [results, setResults] = useState<ResultDTO[]>([]);

  useEffect(() => {
    axios.get<ResultDTO[]>("/api/results/highscores").then((response) => {
      setResults(response.data);
    });
  }, []);

  return (
    <div>
      <Typography variant="h4">Top 10 Results</Typography>
      <List>
        {results.map((result, index) => (
          <ListItem
            key={index}
            style={{
              color: index === 0 ? "gold" : index === 1 ? "silver" : index === 2 ? "bronze" : "black",
            }}
          >
            <ListItemText
              primary={`${result.userEmail}: ${result.score} points`}
              secondary={`Time: ${result.timeInSeconds}s`}
            />
          </ListItem>
        ))}
      </List>
    </div>
  );
};

export default ResultPage;
