import { getToken } from "./authManager";

const strengthWorkoutUrl = '/api/StrengthWorkout'

export const getStrengthWorkoutbyId = (id) => {
    return getToken().then((token) => {
        return fetch(`${strengthWorkoutUrl}/${id}`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).then((resp) => {
            if (resp.ok) {
                return resp.json();
            } else {
                throw new Error(
                    "An unknown error occurred while trying to get the strength workout.",
                );
            }
        });
    });
};

export const addStrengthWorkout = (strengthWorkout) => {
    return getToken().then((token) => {
        return fetch(strengthWorkoutUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(strengthWorkout)
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

export const updateStrengthWorkout = (strengthWorkout) => {
    return getToken().then((token) => {
        return fetch(strengthWorkoutUrl, {
            method: "PUT",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(strengthWorkout)
        }).then((resp) => {
            if (resp.ok) {
                console.log("Workout edited successfully!")
                return resp.json();
            } else {
                throw new Error(
                    "An error occurred while trying to edit a workout",
                );
            }
        });
    });
}

export const deleteStrengthWorkout = (id) => {
    return getToken().then(token => {
        return fetch(`${strengthWorkoutUrl}/${id}`, {
            method: "DELETE",
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
    })
}



