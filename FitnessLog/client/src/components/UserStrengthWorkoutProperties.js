import { useNavigate } from "react-router-dom";
import { deleteUserStrengthWorkout } from "../modules/userStrengthWorkoutManager";


export const UserStrengthWorkoutProperties = ({ userStrengthWorkout }) => {

    const navigate = useNavigate()

    return (
        <div>
            <div>{userStrengthWorkout?.strengthWorkout?.name}</div>
            <div>{userStrengthWorkout?.strengthWorkout?.weight}</div>
            <div>{userStrengthWorkout?.strengthWorkout?.sets}</div>
            <div>{userStrengthWorkout?.strengthWorkout?.reps}</div>
            <button onClick={() => navigate(`/editStrength/${userStrengthWorkout?.strengthWorkout.id}`)}>Edit</button>
            <button onClick={() => deleteUserStrengthWorkout(userStrengthWorkout?.id)}>Delete</button>
        </div>
    )
}