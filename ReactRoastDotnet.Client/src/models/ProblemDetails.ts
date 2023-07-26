interface ProblemDetails {
    title: string;
    status: number;
    detail?: string;
    errors?: Record<string, string[]>
}

export default ProblemDetails;
