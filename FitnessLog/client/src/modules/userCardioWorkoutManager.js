import { getToken } from "./authManager";

const userCardioWorkoutUrl = '/api/UserCardioWorkout/GetMyCardioWorkout/'
const userCardioWorkoutsUrl = 'api/UserCardioWorkout/'

export const getAllCardioWorkouts = () => {
    return getToken().then((token) => {
        return fetch(userCardioWorkoutsUrl, {
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

export const getAllUserCardioWorkouts = () => {
    return getToken().then((token) => {
        return fetch(userCardioWorkoutUrl, {
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

export const addUserCardioWorkout = (userCardioWorkout) => {
    return getToken().then((token) => {
        return fetch(userCardioWorkoutsUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(userCardioWorkout)
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

export const deleteUserCardioWorkout = (id) => {
    return getToken().then(token => {
        return fetch(`${userCardioWorkoutsUrl}/${id}`, {
            method: "DELETE",
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
    })
}