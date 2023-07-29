import { useNavigate } from "react-router-dom"

export const UserStrengthWorkout = ({ userStrengthWorkout }) => {

    const navigate = useNavigate()

    return (
        <div>
            <div>{userStrengthWorkout?.strengthWorkout?.name}</div>
            <div>{userStrengthWorkout?.strengthWorkout?.weight}</div>
            <div>{userStrengthWorkout?.strengthWorkout?.reps}</div>
            <div>{userStrengthWorkout?.strengthWorkout?.sets}</div>
            <button onClick={() => navigate()}>Edit</button>
        </div>
    )
}

export default UserStrengthWorkout