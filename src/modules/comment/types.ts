export type CreateCommentArgs = {
    userId: number;
    text: string;
    mapId: number;
}

export type UpdateCommentArgs = {
    commentId: number;
    text: string;
    userId: number;
}

export type GetCommentsByUserArgs = {
    userId: number;
}

export type GetCommentsByMapArgs = {
    mapId: number;
}

export type RemoveCommentArgs = {
    commentId: number;
}

export type GetCommentArgs = {
    commentId: number;
}