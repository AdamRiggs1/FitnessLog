import { useNavigate } from "react-router-dom";
import { useState, useEffect } from "react"
import { getAllCardioWorkouts } from "../modules/userCardioWorkoutManager";
import { UserCardioWorkoutProperties } from "./UserCardioWorkoutProperties";

export const UserCardioWorkout = () => {

    const [userCardioWorkouts, setUserCardioWorkouts] = useState([])

    const navigate = useNavigate()



    useEffect(() => {
        getAllCardioWorkouts().then(setUserCardioWorkouts)
    }, []);

    return (
        <>
            <div>
                {userCardioWorkouts.map((userCardioWorkout) => (<UserCardioWorkoutProperties userCardioWorkout={userCardioWorkout} key={userCardioWorkout.id} />))}
            </div>
        </>
    );

}

export default UserCardioWorkout