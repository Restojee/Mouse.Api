export type CreateTagArgs = {
    userId: number;
    name: string;
    description?: string;
}

export type UpdateTagArgs = {
    tagId: number;
    name: string;
    description?: string;
}

export type DeleteTagArgs = {
    tagId: number;
}

export type GetTagsByUserArgs = {
    userId: number;
}

export type GetTagsByMapArgs = {
    mapId: number;
}

export type RemoveTagArgs = {
    tagId: number;
}

export type GetTagArgs = {
    tagId: number;
}