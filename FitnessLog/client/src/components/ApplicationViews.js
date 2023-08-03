import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Login from "./Login";
import Register from "./Register";
import Hello from "./Hello";
import WorkoutContainer from "./WorkoutContainer";
import EditCardioWorkoutForm from "./EditCardioWorkoutForm";
import EditStrengthWorkoutForm from "./EditStrengthWorkoutForm";
import FoodList from "./FoodList";
import EditFoodForm from "./EditFoodForm";
import VideoList from "./VideoList";
import AddWorkoutFormContainer from "./AddWorkoutFormContainer";
import AddFoodForm from "./AddFoodForm";
import AddVideoForm from "./AddVideoForm";

export default function ApplicationViews({ isLoggedIn, profile }) {
    return (
        <main>
            <Routes>
                <Route path="/">
                    <Route
                        index
                        element={isLoggedIn ? <Hello /> : <Navigate to="/login" />}
                    />
                    <Route
                        path="hello"
                        element={isLoggedIn ? < Hello /> : <Navigate to="/login" />}
                    />
                    <Route
                        path="workoutList"
                        element={isLoggedIn ? < WorkoutContainer /> : <Navigate to="/login" />}
                    />
                    <Route
                        path="editCardio/:cardioWorkoutId"
                        element={isLoggedIn ? < EditCardioWorkoutForm /> : <Navigate to="/login" />}
                    />
                    <Route
                        path="editStrength/:strengthWorkoutId"
                        element={isLoggedIn ? < EditStrengthWorkoutForm /> : <Navigate to="/login" />}
                    />
                    <Route
                        path="foodList"
                        element={isLoggedIn ? < FoodList /> : <Navigate to="/login" />}
                    />
                    <Route
                        path="editFood/:foodId"
                        element={isLoggedIn ? < EditFoodForm /> : <Navigate to="/login" />}
                    />
                    <Route
                        path="workoutFormContainer"
                        element={isLoggedIn ? < AddWorkoutFormContainer /> : <Navigate to="/login" />}
                    />
                    <Route
                        path="addFoodForm"
                        element={isLoggedIn ? < AddFoodForm /> : <Navigate to="/login" />}
                    />
                    <Route
                        path="videoList"
                        element={isLoggedIn ? < VideoList /> : <Navigate to="/login" />}
                    />
                    <Route
                        path="addVideoForm"
                        element={isLoggedIn ? < AddVideoForm /> : <Navigate to="/login" />}
                    />


                    <Route path="login" element={<Login />} />
                    <Route path="register" element={<Register />} />
                    <Route path="*" element={<p>Whoops, nothing here...</p>} />
                </Route>
            </Routes>
        </main>
    );
};