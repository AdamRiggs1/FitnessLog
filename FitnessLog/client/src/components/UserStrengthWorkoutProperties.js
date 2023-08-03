import { useNavigate } from "react-router-dom";
import { deleteUserStrengthWorkout } from "../modules/userStrengthWorkoutManager";
import "./WorkoutList.css"


export const UserStrengthWorkoutProperties = ({ userStrengthWorkout }) => {

    const navigate = useNavigate()

    return (
        <div className="workout-listItem">
            <div>{userStrengthWorkout?.strengthWorkout?.name}</div>
            <div>{userStrengthWorkout?.strengthWorkout?.weight} lbs./kg </div>
            <div>{userStrengthWorkout?.strengthWorkout?.sets} sets </div>
            <div>{userStrengthWorkout?.strengthWorkout?.reps} reps </div>
            <button className="edit-strengthWorkout-button" onClick={() => navigate(`/editStrength/${userStrengthWorkout?.strengthWorkout.id}`)}>Edit</button>
            <button className="delete-strengthWorkout-button" onClick={() => deleteUserStrengthWorkout(userStrengthWorkout?.id)}>Delete</button>
        </div>
    )
}