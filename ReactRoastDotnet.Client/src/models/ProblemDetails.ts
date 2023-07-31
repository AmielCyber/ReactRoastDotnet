interface ProblemDetails {
    title: string;
    status: number;
    detail?: string;
    errors?: Record<string, string[]>
    type?: string;
    traceId?: string;
}

export default ProblemDetails;
