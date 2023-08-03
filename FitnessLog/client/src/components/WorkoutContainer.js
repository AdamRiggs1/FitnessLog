import { useNavigate } from "react-router-dom";
import React, { useState, useEffect } from "react";
import UserStrengthWorkout from "./UserStrengthWorkout";
import UserCardioWorkout from "./UserCardioWorkout";
import "./WorkoutList.css"

export const WorkoutContainer = () => {

    const StrengthWorkout = UserStrengthWorkout();
    const CardioWorkout = UserCardioWorkout();

    const navigate = useNavigate();

    return (<div>
        <h4 className="strengthWorkout-header">Strength Workouts</h4>
        <div className="workout-listItems">
            {StrengthWorkout}
        </div>
        <h4 className="cardioWorkout-header">Cardio Workouts</h4>
        <div className="workout-listItems">
            {CardioWorkout}
        </div>
        <div className="workout-listButton">
            <button onClick={() => navigate('/workoutFormContainer')}>Add Workout</button>
        </div>
    </div>
    )
}

export default WorkoutContainer