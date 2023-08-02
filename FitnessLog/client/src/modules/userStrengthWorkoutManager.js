import { getToken } from "./authManager";

const userStrengthWorkoutUrl = '/api/UserStrengthWorkout/GetMyStrengthWorkout/'
const userStrengthWorkoutsUrl = '/api/UserStrengthWorkout/'

export const getAllStrengthWorkouts = () => {
    return getToken().then((token) => {
        return fetch(userStrengthWorkoutUrl, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).then((resp) => {
            if (resp.ok) {
                return resp.json();
            } else {
                throw new Error(
                    "An unknown error occurred while trying to get workouts.",
                );
            }
        });
    });
};

export const addUserStrengthWorkout = (userStrengthWorkout) => {
    return getToken().then((token) => {
        return fetch(userStrengthWorkoutUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(userStrengthWorkout)
        }).then((resp) => {
            if (resp.ok) {
                console.log("Workout added successfully!")
                return resp.json();
            } else {
                throw new Error(
                    "An error occurred while trying to add a workout",
                );
            }
        });
    });
}

export const deleteUserStrengthWorkout = (id) => {
    return getToken().then(token => {
        return fetch(`${userStrengthWorkoutsUrl}/${id}`, {
            method: "DELETE",
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
    })
}