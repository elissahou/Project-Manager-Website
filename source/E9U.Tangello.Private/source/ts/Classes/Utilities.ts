export class Utilities{

    public static GetRequestVerificationToken(): string {
        let requestVerificationTokenElement: HTMLInputElement = document.querySelector('input[name="__RequestVerificationToken"]');

        let requestVerificationToken = requestVerificationTokenElement.value;
        return requestVerificationToken;
    }
};