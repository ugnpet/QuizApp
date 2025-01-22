import React, { useState, useEffect } from "react";
import axios from "axios";
import {
  Box,
  Card,
  CardContent,
  List,
  ListItem,
  ListItemText,
  Typography,
  Button,
  Container,
  Grid,
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import { ResultDTO } from "../types";
import dayjs from "dayjs";

const ResultPage: React.FC = () => {
  const [results, setResults] = useState<ResultDTO[]>([]);
  const navigate = useNavigate();

  useEffect(() => {
    axios.get<ResultDTO[]>("/api/results/highscores").then((response) => {
      setResults(response.data);
    });
  }, []);

  const getColor = (index: number) => {
    if (index === 0) return "#FFD700"; // gold
    if (index === 1) return "#C0C0C0"; // silver
    if (index === 2) return "#CD7F32"; // bronze
    return "#E0E0E0";
  };

  return (
    <Container maxWidth="sm" sx={{ mt: 6 }}>
      <Card
        sx={{
          p: 4,
          borderRadius: 3,
          boxShadow: 3,
          background: "linear-gradient(135deg, #f5f7fa 0%, #c3cfe2 100%)",
        }}
      >
        <Grid container spacing={2} alignItems="center" justifyContent="space-between">
          <Grid item>
            <Typography variant="h4" component="h1" sx={{ fontWeight: "bold" }}>
              Top 10 Results
            </Typography>
          </Grid>
          <Grid item>
            <Button
              variant="contained"
              color="primary"
              onClick={() => navigate(-1)}
              sx={{
                textTransform: "none",
                borderRadius: "50px",
                boxShadow: "0px 3px 5px rgba(0,0,0,0.2)",
                ":hover": { boxShadow: "0px 5px 8px rgba(0,0,0,0.3)" },
              }}
            >
              Back
            </Button>
          </Grid>
        </Grid>
        <Box sx={{ mt: 4 }}>
          <List>
            {results.map((result, index) => (
              <ListItem
                key={index}
                sx={{
                  mb: 1.5,
                  borderRadius: 2,
                  backgroundColor: index < 3 ? getColor(index) : "#fff",
                  boxShadow: index < 3 ? "0px 2px 4px rgba(0, 0, 0, 0.2)" : "none",
                  transition: "transform 0.2s",
                  ":hover": { transform: "translateY(-2px)" },
                }}
              >
                <ListItemText
                  primary={
                    <Typography
                      variant="h6"
                      sx={{
                        color: index < 3 ? "#fff" : "text.primary",
                        fontWeight: index < 3 ? "bold" : "medium",
                      }}
                    >
                      {result.userEmail}: {result.score} points
                    </Typography>
                  }
                  secondary={
                    <>
                      <Typography
                        variant="body2"
                        sx={{ color: index < 3 ? "#fff" : "text.secondary" }}
                      >
                        Time: {result.timeInSeconds}s
                      </Typography>
                      <Typography
                        variant="body2"
                        sx={{
                          color: index < 3 ? "#fff" : "text.secondary",
                          fontStyle: "italic",
                        }}
                      >
                        Submitted: {dayjs(result.submittedAt).format("MMM D, YYYY h:mm A")}
                      </Typography>
                    </>
                  }
                />
              </ListItem>
            ))}
          </List>
        </Box>
      </Card>
    </Container>
  );
};

export default ResultPage;
