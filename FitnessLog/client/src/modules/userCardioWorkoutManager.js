import { getToken } from "./authManager";

const userCardioWorkoutUrl = '/api/UserCardioWorkout/GetMyCardioWorkout/'
const userCardioWorkoutsUrl = 'api/UserCardioWorkout/'

export const getAllCardioWorkouts = () => {
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