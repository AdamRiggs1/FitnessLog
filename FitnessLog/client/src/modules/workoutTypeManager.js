import { getToken } from "./authManager";

const workoutTypeUrl = '/api/WorkoutType/'

export const getAllWorkoutTypes = () => {
    return getToken().then((token) => {
        return fetch(workoutTypeUrl, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).then((resp) => {
            if (resp.ok) {
                return resp.json();
            } else {
                throw new Error(
                    "An unknown error occurred while trying to get workout types.",
                );
            }
        });
    });
};